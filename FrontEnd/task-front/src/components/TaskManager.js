import React, { useEffect, useState } from 'react';
import '../styles/TaskManager.css';

const BASE_URL = 'http://localhost:5167/api/Task';

function TaskManager() {
    const [tasks, setTasks] = useState([]);
    const [searchId, setSearchId] = useState('');
    const [notification, setNotification] = useState('');
    const [notFound, setNotFound] = useState(false);

    const [showCreateModal, setShowCreateModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [statusFilter, setStatusFilter] = useState('');
    const [newTask, setNewTask] = useState({ title: '', desc: '' });
    const [editTask, setEditTask] = useState(null);

    useEffect(() => {
        fetchTasks();
    }, []);

    const statusMap = {
        0: 'Pendente',
        1: 'EmProgresso',
        2: 'Concluida'
    };//dicionario de status

    const fetchTasks = async () => {
        const response = await fetch(`${BASE_URL}/getAll`);
        const data = await response.json();
        setTasks(data);
        setNotFound(false);
    };

    const handleSearch = async () => {
        if (!searchId) return fetchTasks();

        const response = await fetch(`${BASE_URL}/getId/${searchId}`);
        if (response.ok) {
            const data = await response.json();
            setTasks([data]);
            setNotFound(false);
        } else {
            setTasks([]);
            setNotFound(true);
        }
    };

    const handleCreate = async () => {
        if (!newTask.title) return;

        const response = await fetch(`${BASE_URL}/create`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newTask)
        });

        if (response.ok) {
            setNotification('Tarefa criada com sucesso!');
            setNewTask({ title: '', desc: '' });
            setShowCreateModal(false);
            fetchTasks();
            clearNotification();
        }
    };

    const handleDelete = async (id) => {
        await fetch(`${BASE_URL}/delete/${id}`, { method: 'DELETE' });
        setNotification('Tarefa deletada com sucesso!');
        fetchTasks();
        clearNotification();
    };

    const handleEditSave = async () => {
        const response = await fetch(`${BASE_URL}/update/${editTask.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(editTask)
        });

        if (response.ok) {
            setNotification('Tarefa atualizada com sucesso!');
            setShowEditModal(false);
            fetchTasks();
            clearNotification();
        }
    };

    const clearNotification = () => {
        setTimeout(() => setNotification(''), 3000);
    };

    return (
        <div className="task-container">
            <img
                src="https://cliente.datasystem.com.br/Logo-Data-System.png"
                alt="Banner Data System"
                className="top-banner"
            />

            {notification && <div className="notification">{notification}</div>}
            {notFound && <div className="not-found">ID não encontrado</div>}

            <div className="task-controls">
                <input
                    type="number"
                    placeholder="Buscar por ID"
                    value={searchId}
                    onChange={e => setSearchId(e.target.value)}
                />
                <button onClick={handleSearch}>Buscar</button>
                <button onClick={() => setShowCreateModal(true)}>Criar Nova</button>

                <select
                    value={statusFilter}
                    onChange={async (e) => {
                        const selected = e.target.value;
                        setStatusFilter(selected);

                        if (selected === '') {
                            fetchTasks();
                        } else {
                            const response = await fetch(`${BASE_URL}/filterStatus/${selected}`);
                            if (response.ok) {
                                const data = await response.json();
                                setTasks(data);
                                setNotFound(data.length === 0);
                            }
                        }
                    }}
                >
                    <option value="">Filtrar por Status</option>
                    <option value="0">Pendente</option>
                    <option value="1">EmProgresso</option>
                    <option value="2">Concluida</option>
                </select>
            </div>

            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Título</th>
                        <th>Status</th>
                        <th>Data de Criação</th>
                        <th>Ações</th>
                    </tr>
                </thead>
                <tbody>
                    {tasks.map(task => (
                        <tr key={task.id}>
                            <td>{task.id}</td>
                            <td>{task.title}</td>
                            <td>{statusMap[task.status]}</td>
                            <td>{new Date(task.dtCreate).toLocaleDateString()}</td>
                            <td>
                                <button onClick={() => { setEditTask(task); setShowEditModal(true); }}>
                                    Editar
                                </button>
                                <button onClick={() => handleDelete(task.id)}>Deletar</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            {showCreateModal && (
                <div className="modal-overlay">
                    <div className="modal-content">
                        <button className="close-button" onClick={() => setShowCreateModal(false)}>X</button>
                        <h2>Criar Tarefa</h2>
                        <input
                            type="text"
                            placeholder="Título"
                            value={newTask.title}
                            onChange={e => setNewTask({ ...newTask, title: e.target.value })}
                        />
                        <textarea
                            placeholder="Descrição"
                            value={newTask.desc}
                            onChange={e => setNewTask({ ...newTask, desc: e.target.value })}
                        />
                        <button onClick={handleCreate}>Criar</button>
                    </div>
                </div>
            )}

            {showEditModal && editTask && (
                <div className="modal-overlay">
                    <div className="modal-content">
                        <button className="close-button" onClick={() => setShowEditModal(false)}>X</button>
                        <h2>Editar Tarefa</h2>
                        <input
                            type="text"
                            placeholder="Título"
                            value={editTask.title}
                            onChange={e => setEditTask({ ...editTask, title: e.target.value })}
                        />
                        <textarea
                            placeholder="Descrição"
                            value={editTask.desc || ''}
                            onChange={e => setEditTask({ ...editTask, desc: e.target.value })}
                        />
                        <select
                            value={editTask.status}
                            onChange={e => setEditTask({ ...editTask, status: parseInt(e.target.value) })}
                        >
                            <option value={0}>Pendente</option>
                            <option value={1}>EmProgresso</option>
                            <option value={2}>Concluida</option>
                        </select>
                        <button onClick={handleEditSave}>Salvar</button>
                    </div>
                </div>
            )}
        </div>
    );
}

export default TaskManager;