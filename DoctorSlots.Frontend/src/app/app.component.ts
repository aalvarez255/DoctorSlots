import { Component } from '@angular/core'

@Component({
  selector: 'app-root',
  template: `
      <header>
          <app-navbar></app-navbar>
      </header>
      <main class="main">
          <router-outlet></router-outlet>
      </main>`
})

export class AppComponent {
}
