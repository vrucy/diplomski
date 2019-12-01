import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistracijaMajstoraComponent } from './registracija-majstora.component';

describe('RegistracijaMajstoraComponent', () => {
  let component: RegistracijaMajstoraComponent;
  let fixture: ComponentFixture<RegistracijaMajstoraComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistracijaMajstoraComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistracijaMajstoraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
