import { TestBed } from '@angular/core/testing';

import { AstronautDutyRestService } from './astronaut-duty-rest-service';

describe('AstronautDutyRestService', () => {
  let service: AstronautDutyRestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AstronautDutyRestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
