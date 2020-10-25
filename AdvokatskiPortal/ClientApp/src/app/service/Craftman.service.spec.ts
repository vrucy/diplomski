import { TestBed } from '@angular/core/testing';

import { CraftmanService } from './Craftman.service';

describe('CraftmanService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CraftmanService = TestBed.get(CraftmanService);
    expect(service).toBeTruthy();
  });
});
