import { Routes } from '@angular/router'

export const appRoutes: Routes = [
	{ path: '', loadChildren: './slots/slots.module#SlotsModule' }
]
