import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PocetnaStranicaMajstorComponent } from './pocetna-stranica-majstor.component';

describe('PocetnaStranicaMajstorComponent', () => {
  let component: PocetnaStranicaMajstorComponent;
  let fixture: ComponentFixture<PocetnaStranicaMajstorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PocetnaStranicaMajstorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PocetnaStranicaMajstorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
