import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationCraftmanComponent } from './registration-craftman.component';

describe('RegistrationCraftmanComponent', () => {
  let component: RegistrationCraftmanComponent;
  let fixture: ComponentFixture<RegistrationCraftmanComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistrationCraftmanComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrationCraftmanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
