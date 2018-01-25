import { async, ComponentFixture, TestBed } from '@angular/core/testing'
import { AppNavbarComponent } from './app-navbar.component'
import { TranslateModule, TranslateLoader } from '@ngx-translate/core'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/observable/of'

class FakeLoader implements TranslateLoader {
    getTranslation(lang: string): Observable<any> {
        return Observable.of({})
    }
}

describe('AppNavbarComponent', () => {
    let component: AppNavbarComponent
    let fixture: ComponentFixture<AppNavbarComponent>

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [AppNavbarComponent],
            imports: [
                TranslateModule.forRoot({
                    loader: { provide: TranslateLoader, useClass: FakeLoader },
                })]
        })
            .compileComponents()
    }))

    beforeEach(() => {
        fixture = TestBed.createComponent(AppNavbarComponent)
        component = fixture.componentInstance
        fixture.detectChanges()
    })

    it('should create', () => {
        expect(component).toBeTruthy()
    })

    it('should display logo', () => {
        expect(fixture.nativeElement.querySelector(".brand img").src).not.toBeUndefined()
    })

    it('should define 3 languages', () => {
        expect(component.availableLanguages.length).toBe(3)
    })
})
