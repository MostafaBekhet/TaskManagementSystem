import { TestBed } from '@angular/core/testing';

import { AuthintecationService } from './authintecation.service';

describe('AuthintecationService', () => {
  let service: AuthintecationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthintecationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
