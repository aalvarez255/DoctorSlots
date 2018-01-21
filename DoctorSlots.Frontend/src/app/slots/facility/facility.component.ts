import { Component, OnInit } from '@angular/core'
import { Input } from '@angular/core'
import { Facility } from '../../shared/models/Facility'

@Component({
  selector: 'facility',
  templateUrl: './facility.component.html',
  styleUrls: ['./facility.component.less']
})
export class FacilityComponent implements OnInit {

  @Input() facility: Facility

  constructor() { }

  ngOnInit() {
  }
}
