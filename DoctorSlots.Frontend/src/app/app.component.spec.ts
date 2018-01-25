import { TestBed, async } from '@angular/core/testing'
import { RouterTestingModule } from '@angular/router/testing'
import { AppComponent } from './app.component'
import { AppNavbarComponent } from './shared/components/app-navbar/app-navbar.component'
import { RouterModule } from '@angular/router'
import { TranslateModule, TranslateLoader } from '@ngx-translate/core'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/observable/of'

class FakeLoader implements TranslateLoader {
    getTranslation(lang: string): Observable<any> {
        return Observable.of({})
    }
}

describe('AppComponent', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [
                AppComponent,
                AppNavbarComponent
            ],
            imports: [
                RouterTestingModule.withRoutes([]),
                TranslateModule.forRoot({
                    loader: { provide: TranslateLoader, useClass: FakeLoader },
                })
            ]
        }).compileComponents()
    }))
    it('should create the app', async(() => {
        const fixture = TestBed.createComponent(AppComponent)
        const app = fixture.debugElement.componentInstance
        expect(app).toBeTruthy()
    }))
})
