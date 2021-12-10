import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BottomOutlineComponent } from './bottom-outline.component';

describe('BottomToolbarComponent', () => {
  let component: BottomOutlineComponent;
  let fixture: ComponentFixture<BottomOutlineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BottomOutlineComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BottomOutlineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
