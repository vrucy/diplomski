import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PreviewCaseComponent } from './preview-case.component';

describe('PregledSlucajaKorisnikComponent', () => {
  let component: PreviewCaseComponent;
  let fixture: ComponentFixture<PreviewCaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PreviewCaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PreviewCaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
