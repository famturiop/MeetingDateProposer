import { TestBed } from '@angular/core/testing';

import { OpenNewWindowService } from './open-new-window.service';

describe('OpenNewWindowService', () => {
  let service: OpenNewWindowService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OpenNewWindowService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
