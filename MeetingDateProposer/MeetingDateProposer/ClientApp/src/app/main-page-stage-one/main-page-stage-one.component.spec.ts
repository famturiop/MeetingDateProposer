import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainPageStageOneComponent } from './main-page-stage-one.component';

describe('MainPageStageOneComponent', () => {
  let component: MainPageStageOneComponent;
  let fixture: ComponentFixture<MainPageStageOneComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MainPageStageOneComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MainPageStageOneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
