import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistracijaAdvokataComponent } from './registracija-advokata.component';

describe('RegistracijaAdvokataComponent', () => {
  let component: RegistracijaAdvokataComponent;
  let fixture: ComponentFixture<RegistracijaAdvokataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistracijaAdvokataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistracijaAdvokataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
