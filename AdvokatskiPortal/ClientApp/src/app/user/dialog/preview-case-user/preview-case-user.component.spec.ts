import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PreviewCaseUserComponent } from './preview-case-user.component';

describe('PreviewCaseUserComponent', () => {
  let component: PreviewCaseUserComponent;
  let fixture: ComponentFixture<PreviewCaseUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PreviewCaseUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PreviewCaseUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
