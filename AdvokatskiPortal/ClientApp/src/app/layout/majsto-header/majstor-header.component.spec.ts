import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MajstorHeaderComponent } from './majstor-header.component';

describe('MajstorHeaderComponent', () => {
  let component: MajstorHeaderComponent;
  let fixture: ComponentFixture<MajstorHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MajstorHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MajstorHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
