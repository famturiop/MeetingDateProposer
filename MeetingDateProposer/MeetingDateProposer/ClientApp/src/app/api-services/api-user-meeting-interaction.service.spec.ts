import { TestBed } from '@angular/core/testing';

import { ApiUserMeetingInteractionService } from './api-user-meeting-interaction.service';

describe('ApiUserMeetingInteractionService', () => {
  let service: ApiUserMeetingInteractionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiUserMeetingInteractionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
