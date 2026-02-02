import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TripsControlComponent } from './trips-control.component';

describe('TripsControlComponent', () => {
  let component: TripsControlComponent;
  let fixture: ComponentFixture<TripsControlComponent>;
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TripsControlComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TripsControlComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
