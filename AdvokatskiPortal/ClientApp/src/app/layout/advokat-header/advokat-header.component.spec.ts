import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvokatHeaderComponent } from './advokat-header.component';

describe('AdvokatHeaderComponent', () => {
  let component: AdvokatHeaderComponent;
  let fixture: ComponentFixture<AdvokatHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdvokatHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdvokatHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
