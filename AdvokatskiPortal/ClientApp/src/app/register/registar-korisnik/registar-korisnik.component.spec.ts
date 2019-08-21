import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistarKorisnikComponent } from './registar-korisnik.component';

describe('RegistarKorisnikComponent', () => {
  let component: RegistarKorisnikComponent;
  let fixture: ComponentFixture<RegistarKorisnikComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistarKorisnikComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistarKorisnikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
