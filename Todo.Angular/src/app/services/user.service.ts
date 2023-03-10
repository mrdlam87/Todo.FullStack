import { EventEmitter, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../user/user.model';
import { Subject } from 'rxjs';
import { getFormattedDate } from '../shared/utils/date';
import { Todo } from '../todo/todo.model';
import { TodoDto } from '../todo/todo-dto.model';
import { UserDto } from '../user/user-dto.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  usersChanged = new EventEmitter();
  todosChanged = new EventEmitter();
  userSelected = new Subject<User>();
  private currentUser: User;
  private currentUserTodo: Todo;

  constructor(private http: HttpClient) {}

  // User API methods
  getUsers() {
    return this.http.get<User[]>('/api/users');
  }

  addUser(user: UserDto) {
    this.http
      .post('/api/users', user)
      .subscribe(() => this.usersChanged.emit());
  }

  updateUser(user: UserDto) {
    this.http
      .put(`/api/users/${this.currentUser.id}`, user)
      .subscribe(() => this.usersChanged.emit());
  }

  deleteUser() {
    this.http
      .delete(`/api/users/${this.currentUser.id}`)
      .subscribe(() => this.usersChanged.emit());
  }

  // Todo API methods
  getUserTodos() {
    return this.http.get<Todo[]>(`/api/users/${this.currentUser.id}/todos`);
  }

  addUserTodo(todo: TodoDto) {
    this.http
      .post(`/api/users/${this.currentUser.id}/todos`, todo)
      .subscribe(() => this.todosChanged.emit());
  }

  updateUserTodo(todo: TodoDto) {
    this.http
      .put(
        `/api/users/${this.currentUser.id}/todos/${this.currentUserTodo.id}`,
        todo
      )
      .subscribe(() => this.todosChanged.emit());
  }

  deleteUserTodo() {
    this.http
      .delete(
        `/api/users/${this.currentUser.id}/todos/${this.currentUserTodo.id}`
      )
      .subscribe(() => this.todosChanged.emit());
  }

  // Application methods
  getCurrentUser() {
    return this.currentUser;
  }

  setCurrentUser(user: User) {
    this.currentUser = user;
    this.userSelected.next(this.currentUser);
  }

  getCurrentUserTodo() {
    return this.currentUserTodo;
  }

  setCurrentUserTodo(todo: Todo) {
    this.currentUserTodo = todo;
  }
}
