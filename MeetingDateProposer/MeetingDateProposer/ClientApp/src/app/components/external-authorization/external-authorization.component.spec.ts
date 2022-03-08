import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalAuthorizationComponent } from './external-authorization.component';

describe('ExternalAuthorizationComponent', () => {
  let component: ExternalAuthorizationComponent;
  let fixture: ComponentFixture<ExternalAuthorizationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExternalAuthorizationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExternalAuthorizationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
