import { TestBed } from '@angular/core/testing';

import { ApiCalculatorService } from './api-calculator.service';

describe('ApiUserMeetingInteractionService', () => {
  let service: ApiCalculatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiCalculatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
