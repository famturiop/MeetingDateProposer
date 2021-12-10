import { TestBed } from '@angular/core/testing';

import { StageTwoService } from './stage-two.service';

describe('StageTwoService', () => {
  let service: StageTwoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StageTwoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
