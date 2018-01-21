import { Component, OnInit} from '@angular/core'
import { FacilitySlots } from '../shared/models/FacilitySlots';
import { SlotsApiService } from '../shared/services/slots-api.service';

declare var SocialWall: any;

@Component({
	selector: 'slots',
	templateUrl: './slots.component.html'
})

export class SlotsComponent implements OnInit {

	facilitySlots: FacilitySlots

	constructor(private _api: SlotsApiService) { }

	ngOnInit() {		
		this._api.getFacilitySlots(new Date()).then(data => {
			this.facilitySlots = <FacilitySlots>data
			console.log(data)
			console.log(this.facilitySlots)
		})
	}
}
