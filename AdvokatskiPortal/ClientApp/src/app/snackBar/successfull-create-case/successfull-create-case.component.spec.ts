import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SuccessfullCreateCaseComponent } from './successfull-create-case.component';

describe('SuccessfullCreateCaseComponent', () => {
  let component: SuccessfullCreateCaseComponent;
  let fixture: ComponentFixture<SuccessfullCreateCaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SuccessfullCreateCaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SuccessfullCreateCaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
