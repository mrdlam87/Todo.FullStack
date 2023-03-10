import { Todo } from '../todo/todo.model';

export interface User {
  id: string;
  fullName: string;
  todos: Todo[];
}
