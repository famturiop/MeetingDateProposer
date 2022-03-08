import { TestBed } from '@angular/core/testing';

import { ApiMeetingService } from './api-meeting.service';

describe('ApiMeetingService', () => {
  let service: ApiMeetingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiMeetingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
