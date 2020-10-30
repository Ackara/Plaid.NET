/// <reference path="../../../node_modules/@types/knockout/index.d.ts" />
/// <reference path="../../../typings/server.d.ts" />
/// <reference path="../components/Clipboard.ts" />
/// <reference path="../domain/Server.ts" />

declare var Plaid: any;

namespace PlaidLink {
	interface PageModel {
		token: string;
		accessToken: string;
	}

	export class IndexPage {
		constructor(model: PageModel) {
			let me = this;
			this.accessToken = ko.observable(model.accessToken);
			console.debug(model);
			// Plaid Configuration

			let handler = Plaid.create({
				token: model.token,
				onSuccess: function (public_token, metadata) {
					console.debug(metadata);
					Server.getAccesssToken(public_token, metadata)
						.then(function (data) {
							console.debug(data);
							me.accessToken(data.accessToken);
							Clipboard.copy(data.accessToken);
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

		// ==================== Event Handlers ==================== //

		public copyToClipboardButtonClicked(): void {
			Clipboard.copy(this.accessToken());
		}
	}
}
