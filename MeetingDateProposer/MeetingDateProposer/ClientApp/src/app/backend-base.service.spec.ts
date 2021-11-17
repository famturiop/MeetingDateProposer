import { TestBed } from '@angular/core/testing';

import { BackendBaseService } from './backend-base.service';

describe('BackendBaseService', () => {
  let service: BackendBaseService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BackendBaseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
