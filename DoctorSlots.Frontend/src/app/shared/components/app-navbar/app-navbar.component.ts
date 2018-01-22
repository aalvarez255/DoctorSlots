import { Component, OnInit } from '@angular/core'
import { TranslateService } from '@ngx-translate/core'

@Component({
	selector: 'app-navbar',
	templateUrl: './app-navbar.component.html',
	styleUrls: ['./app-navbar.component.less']
})
export class AppNavbarComponent implements OnInit {

	currentLanguage: string
	availableLanguages: string[]

	constructor(
		private _translateService: TranslateService
	) {}

	ngOnInit() {
		this.currentLanguage = 'en'
		this.availableLanguages = ['ca', 'es', 'en']

		this._translateService.addLangs(this.availableLanguages)
		this._translateService.use(this.currentLanguage)
	}

	selectLanguage(lang: string) {
		this.currentLanguage = lang
		this._translateService.use(lang)
	}
}
