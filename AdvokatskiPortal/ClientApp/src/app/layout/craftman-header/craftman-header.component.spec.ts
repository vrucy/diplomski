import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CraftmanHeaderComponent } from './craftman-header.component';

describe('CraftmanHeaderComponent', () => {
  let component: CraftmanHeaderComponent;
  let fixture: ComponentFixture<CraftmanHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CraftmanHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CraftmanHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
