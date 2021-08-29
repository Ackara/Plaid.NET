namespace PlaidLink {
	export class Clipboard {
		public static bindToElements(element?: HTMLElement): void {
			if (!element) { element = document.body; }

			document.body.addEventListener("click", function (e: MouseEvent) {
				let sourceElement = <HTMLInputElement>e.target;

				for (let i = 0; i < 5; i++) {
					if (sourceElement.getAttribute("data-can-copy")) {
						Clipboard.copy(sourceElement.value || sourceElement.textContent || sourceElement.innerText);
						return;
					}

					sourceElement = <HTMLInputElement>sourceElement.parentElement;
				}
			});
		}

		public static copy(text: string): string {
			let temp = document.createElement("textarea");
			temp.value = text;
			document.body.appendChild(temp);
			temp.select();
			document.execCommand("copy");
			document.body.removeChild(temp);

			console.debug(`copied '${text}' to clipboard`);

			return text;
		}

		public static copyElementText(element: HTMLElement): string {
			let text = null;
			if (element && element.innerText) {
				this.copy(element.innerText.trim());
			}

			return text;
		}
	}
}

PlaidLink.Clipboard.bindToElements();
