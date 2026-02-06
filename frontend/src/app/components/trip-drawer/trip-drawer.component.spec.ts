import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TripDrawerComponent } from './trip-drawer.component';

describe('TripDrawerComponent', () => {
  let component: TripDrawerComponent;
  let fixture: ComponentFixture<TripDrawerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TripDrawerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TripDrawerComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
