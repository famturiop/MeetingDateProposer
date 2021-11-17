import { TestBed } from '@angular/core/testing';

import { StageOneService } from './stage-one.service';

describe('StageOneService', () => {
  let service: StageOneService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StageOneService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
