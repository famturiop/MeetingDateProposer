import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainPageStageTwoComponent } from './main-page-stage-two.component';

describe('MainPageStageTwoComponent', () => {
  let component: MainPageStageTwoComponent;
  let fixture: ComponentFixture<MainPageStageTwoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MainPageStageTwoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MainPageStageTwoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
