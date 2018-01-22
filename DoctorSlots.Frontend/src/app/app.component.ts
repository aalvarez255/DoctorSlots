import { Component } from '@angular/core'

@Component({
    selector: 'app-root',
    template: `
      <header>
        <app-navbar></app-navbar>
      </header>
      <main class="main">
        <div class="container">
          <router-outlet></router-outlet>
        </div>
      </main>`
})

export class AppComponent {
}
