import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KorisnikHeaderComponent } from './korisnik-header.component';

describe('KorisnikHeaderComponent', () => {
  let component: KorisnikHeaderComponent;
  let fixture: ComponentFixture<KorisnikHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KorisnikHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KorisnikHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
