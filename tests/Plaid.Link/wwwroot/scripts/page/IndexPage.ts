/// <reference path="../../../node_modules/@types/knockout/index.d.ts" />
/// <reference path="../../../typings/server.d.ts" />
/// <reference path="../domain/Server.ts" />

declare var Plaid: any;

namespace App {
	interface PageModel {
		token: string;
	}

	export class IndexPage {
		constructor(model: PageModel) {
			let me = this;
			this.accessToken = ko.observable("");


			// Plaid Configuration

			let handler = Plaid.create({
				token: model.token,
				onSuccess: function (public_token, metadata) {
					console.debug(metadata);
					Server.getAccesssToken(public_token, metadata)
						.then(function (data) {
							console.debug(data);
							me.accessToken(data.accessToken);
						});
				},
				onExit: function (error, metadata) {
				}
			});

			document.getElementById("link-button").addEventListener("click", function () {
				handler.open();
			});
		}

		public accessToken: KnockoutObservable<string>;
	}
}
