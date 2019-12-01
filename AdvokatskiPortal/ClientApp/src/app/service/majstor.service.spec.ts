import { TestBed } from '@angular/core/testing';

import { MajstorService } from './majstor.service';

describe('MajstorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MajstorService = TestBed.get(MajstorService);
    expect(service).toBeTruthy();
  });
});
