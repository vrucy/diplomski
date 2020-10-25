import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SuccessfullRegistrationComponent } from './successfull-registration.component';

describe('SuccessfullRegistrationComponent', () => {
  let component: SuccessfullRegistrationComponent;
  let fixture: ComponentFixture<SuccessfullRegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SuccessfullRegistrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SuccessfullRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
