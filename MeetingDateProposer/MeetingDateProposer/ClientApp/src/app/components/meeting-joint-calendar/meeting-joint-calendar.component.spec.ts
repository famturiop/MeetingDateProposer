import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingJointCalendarComponent } from './meeting-joint-calendar.component';

describe('MeetingJointCalendarComponent', () => {
  let component: MeetingJointCalendarComponent;
  let fixture: ComponentFixture<MeetingJointCalendarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MeetingJointCalendarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MeetingJointCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
