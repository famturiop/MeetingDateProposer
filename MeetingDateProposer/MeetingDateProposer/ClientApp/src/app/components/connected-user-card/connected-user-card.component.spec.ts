import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConnectedUserCardComponent } from './connected-user-card.component';

describe('CalendarAdderComponent', () => {
  let component: ConnectedUserCardComponent;
  let fixture: ComponentFixture<ConnectedUserCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConnectedUserCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConnectedUserCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
