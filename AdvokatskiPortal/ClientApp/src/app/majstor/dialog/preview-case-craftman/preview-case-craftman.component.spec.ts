import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PreviewCaseCraftmanComponent } from './preview-case-craftman.component';

describe('PreviewCaseCraftmanComponent', () => {
  let component: PreviewCaseCraftmanComponent;
  let fixture: ComponentFixture<PreviewCaseCraftmanComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PreviewCaseCraftmanComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PreviewCaseCraftmanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
