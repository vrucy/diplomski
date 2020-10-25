import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SuccessfullLoginComponent } from './successfull-login.component';

describe('SuccessfullLoginComponent', () => {
  let component: SuccessfullLoginComponent;
  let fixture: ComponentFixture<SuccessfullLoginComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SuccessfullLoginComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SuccessfullLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
