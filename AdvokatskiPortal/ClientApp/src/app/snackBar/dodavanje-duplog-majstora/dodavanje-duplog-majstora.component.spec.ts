import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DodavanjeDuplogMajstoraComponent } from './dodavanje-duplog-majstora.component';

describe('DodavanjeDuplogMajstoraComponent', () => {
  let component: DodavanjeDuplogMajstoraComponent;
  let fixture: ComponentFixture<DodavanjeDuplogMajstoraComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DodavanjeDuplogMajstoraComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DodavanjeDuplogMajstoraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
