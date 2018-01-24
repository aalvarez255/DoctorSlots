import { BrowserModule } from '@angular/platform-browser'
import { NgModule } from '@angular/core'
import { HttpClientModule, HttpClient } from '@angular/common/http'
import { RouterModule } from '@angular/router'

import { TranslateModule, TranslateLoader } from '@ngx-translate/core'
import { TranslateHttpLoader } from '@ngx-translate/http-loader'

import { SharedModule } from './shared/shared.module'

import { AppComponent } from './app.component'

import { appRoutes } from './routes'
import { AppHttpService } from './shared/services/app-http.service'
import { SlotsApiService } from './shared/services/slots-api.service'

export const createTranslateLoader = (http: HttpClient) => {
    return new TranslateHttpLoader(http, './assets/i18n/', '.json')
}

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        RouterModule.forRoot(appRoutes),
        BrowserModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: createTranslateLoader,
                deps: [HttpClient]
            }
        }),
        SharedModule
    ],
    providers: [
        AppHttpService,
        SlotsApiService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
