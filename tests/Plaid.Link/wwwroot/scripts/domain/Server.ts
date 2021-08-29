/// <reference path="../../../typings/server.d.ts" />

namespace PlaidLink {

	export interface HttpHeader { name: string, value: string }
	export interface ServerError<T> { status: number, url: string, data: T, message: string }

	export class ServerPromise<TData, TError> {
		constructor(action: (pass: (data: any) => void, reject: (data: any) => void) => void) {
			action(this.pass.bind(this), this.reject.bind(this));
		}

		private thenCallback: (data: any) => void;
		private catchCallback: (data: any) => void;
		private finalCallback: () => void;

		public then(callback: (data: TData) => void): ServerPromise<TData, TError> {
			this.thenCallback = callback;
			return this;
		}

		public catch(callback: (data: TError) => void): ServerPromise<TData, TError> {
			this.catchCallback = callback;
			return this;
		}

		public finally(callback: () => void): void {
			this.finalCallback = callback;
		}

		private pass(data: any): void {
			if (this.thenCallback) { this.thenCallback(data || true); }
			if (this.finalCallback) { this.finalCallback(); }
		}

		private reject(data: any): void {
			if (this.catchCallback) { this.catchCallback(data); }
			if (this.finalCallback) { this.finalCallback(); }
		}
	}

	export class Server {
		public static getAccesssToken(token: string, metadata: any): ServerPromise<any, ServerError<any>> {
			metadata.publicToken = token;
			return this.sendHttpRequest("POST", `Plaid/GetAccessToken`, {
				body: metadata
			});
		}

		public static sendHttpRequest<TData, TError>(method: string, url: string, options?: { headers?: HttpHeader[], body?: any, contentType?: string }): ServerPromise<TData, TError> {
			return new ServerPromise(function (pass, reject) {
				const application_json = "application/json; charset=utf-8";

				if (!options) { options = {}; }
				if (!options.hasOwnProperty("contentType")) { options.contentType = (typeof options.body === "object" ? application_json : null); }

				let request = new XMLHttpRequest();
				request.open(method, url, true);
				if (options.contentType) { request.setRequestHeader("Content-Type", options.contentType); }

				if (options.headers) {
					for (let i = 0; i < options.headers.length; i++) {
						request.setRequestHeader(options.headers[i].name, options.headers[i].value);
					}
				}

				request.onreadystatechange = function () {
					if (this.readyState === 4) {
						let result: any = (this.getResponseHeader("Content-Type") === application_json ? JSON.parse(this.responseText) : this.responseText);

						if (this.status >= 200 && this.status <= 299) {
							pass(result);
							if (!result) { console.warn(`${method} ${this.responseURL}: NO CONTENT`); }
						}

						else {
							reject(<ServerError<any>>{
								status: this.status, url: this.responseURL, data: result,
								message: `${method} ${this.responseURL}: ${this.status} ${(typeof result !== 'object' ? this.responseText : '')}`.trim()
							});
							console.debug(result);
						}
					}
				};
				request.onerror = function () {
					reject(<ServerError<any>>{ message: "Network Error" });
				}

				if (options.contentType === application_json) { request.send(JSON.stringify(options.body)); }
				else { request.send(options.body); }
			});
		}
	}
}
