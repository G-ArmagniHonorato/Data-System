import logo from './logo.svg';
import TaskManager from './components/TaskManager';
import './App.css';

function App() {
    return (
        <div className="App">
            <header className="App-header">
                <h1>Gerenciador de Tarefas</h1>
                <TaskManager />
            </header>
        </div>
    );
}

export default App;
