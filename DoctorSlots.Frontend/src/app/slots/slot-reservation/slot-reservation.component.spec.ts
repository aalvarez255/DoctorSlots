import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SlotReservationComponent } from './slot-reservation.component';

describe('SlotReservationComponent', () => {
  let component: SlotReservationComponent;
  let fixture: ComponentFixture<SlotReservationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SlotReservationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SlotReservationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
