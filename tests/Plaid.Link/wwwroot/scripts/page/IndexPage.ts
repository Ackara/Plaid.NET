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

			this._dropIn = Plaid.create({
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

			if (!model.accessToken) {
				this.onRefreshButtonClicked();
			}
		}

		private _dropIn: any;
		public accessToken: KnockoutObservable<string>;

		// ==================== Event Handlers ==================== //

		public onCopyToClipboardButtonClicked(): void {
			Clipboard.copy(this.accessToken());
		}

		public onRefreshButtonClicked(): void {
			this._dropIn.open();
		}
	}
}
