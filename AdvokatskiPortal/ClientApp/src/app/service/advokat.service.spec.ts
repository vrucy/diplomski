import { TestBed } from '@angular/core/testing';

import { AdvokatService } from './advokat.service';

describe('AdvokatService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AdvokatService = TestBed.get(AdvokatService);
    expect(service).toBeTruthy();
  });
});
