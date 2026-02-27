import { useState, useEffect, useRef, useCallback } from 'react';
import { Play, Pause, RotateCcw, Coffee, BookOpen, SkipForward } from 'lucide-react';

const MODES = [
  { label: 'Foco', minutes: 25, color: '#6366f1', icon: BookOpen },
  { label: 'Pausa Curta', minutes: 5, color: '#22c55e', icon: Coffee },
  { label: 'Pausa Longa', minutes: 15, color: '#8b5cf6', icon: Coffee },
];

export default function PomodoroPage() {
  const [modeIdx, setModeIdx] = useState(0);
  const [seconds, setSeconds] = useState(MODES[0].minutes * 60);
  const [running, setRunning] = useState(false);
  const [sessions, setSessions] = useState(0);
  const intervalRef = useRef(null);

  const mode = MODES[modeIdx];
  const total = mode.minutes * 60;
  const progress = ((total - seconds) / total) * 100;

  const tick = useCallback(() => {
    setSeconds((s) => {
      if (s <= 1) {
        clearInterval(intervalRef.current);
        intervalRef.current = null;
        setRunning(false);
        // Auto advance
        if (modeIdx === 0) {
          setSessions((p) => p + 1);
          // Every 4 sessions → long break
          const next = (sessions + 1) % 4 === 0 ? 2 : 1;
          setModeIdx(next);
          setSeconds(MODES[next].minutes * 60);
        } else {
          setModeIdx(0);
          setSeconds(MODES[0].minutes * 60);
        }
        try { new Audio('data:audio/wav;base64,UklGRl9vT19teleAQBxAF9vT19WAVEZm10LgAAAG').play().catch(() => {}); } catch {}
        return 0;
      }
      return s - 1;
    });
  }, [modeIdx, sessions]);

  useEffect(() => {
    if (running) {
      intervalRef.current = setInterval(tick, 1000);
    }
    return () => { if (intervalRef.current) clearInterval(intervalRef.current); };
  }, [running, tick]);

  const toggleRun = () => setRunning((r) => !r);

  const reset = () => {
    setRunning(false);
    if (intervalRef.current) clearInterval(intervalRef.current);
    setSeconds(mode.minutes * 60);
  };

  const switchMode = (idx) => {
    setRunning(false);
    if (intervalRef.current) clearInterval(intervalRef.current);
    setModeIdx(idx);
    setSeconds(MODES[idx].minutes * 60);
  };

  const skip = () => {
    setRunning(false);
    if (intervalRef.current) clearInterval(intervalRef.current);
    if (modeIdx === 0) {
      setSessions((p) => p + 1);
      const next = (sessions + 1) % 4 === 0 ? 2 : 1;
      setModeIdx(next);
      setSeconds(MODES[next].minutes * 60);
    } else {
      setModeIdx(0);
      setSeconds(MODES[0].minutes * 60);
    }
  };

  const mm = String(Math.floor(seconds / 60)).padStart(2, '0');
  const ss = String(seconds % 60).padStart(2, '0');
  const IconM = mode.icon;

  return (
    <div className="p-4 md:p-6 lg:p-8 max-w-2xl mx-auto space-y-6 lg:space-y-8">
      {/* Header */}
      <div className="text-center">
        <h1 className="text-xl md:text-2xl font-bold text-white flex items-center justify-center gap-2 lg:gap-3">
          <BookOpen className="w-6 h-6 lg:w-7 lg:h-7 text-indigo-400" /> Pomodoro Timer
        </h1>
        <p className="text-slate-400 text-xs md:text-sm mt-1">Técnica de estudo com intervalos focados de 25 minutos.</p>
      </div>

      {/* Mode Tabs */}
      <div className="flex justify-center gap-1.5 lg:gap-2">
        {MODES.map((m, i) => (
          <button key={i} onClick={() => switchMode(i)}
            className={`px-3 md:px-4 py-2 rounded-xl text-xs md:text-sm font-medium transition cursor-pointer active:scale-95 ${
              modeIdx === i
                ? 'text-white'
                : 'bg-slate-800/50 text-slate-400 hover:text-white border border-slate-700/50'
            }`}
            style={modeIdx === i ? { backgroundColor: m.color } : undefined}>
            {m.label}
          </button>
        ))}
      </div>

      {/* Timer Circle */}
      <div className="flex justify-center">
        <div className="relative w-72 h-72">
          {/* SVG Circle */}
          <svg className="w-full h-full -rotate-90" viewBox="0 0 200 200">
            <circle cx="100" cy="100" r="90" fill="none" stroke="#1e293b" strokeWidth="6" />
            <circle cx="100" cy="100" r="90" fill="none"
              stroke={mode.color} strokeWidth="6" strokeLinecap="round"
              strokeDasharray={`${2 * Math.PI * 90}`}
              strokeDashoffset={`${2 * Math.PI * 90 * (1 - progress / 100)}`}
              className="transition-all duration-1000" />
          </svg>
          {/* Center content */}
          <div className="absolute inset-0 flex flex-col items-center justify-center">
            <div className="p-2 rounded-full mb-2" style={{ backgroundColor: mode.color + '22' }}>
              <IconM className="w-5 h-5" style={{ color: mode.color }} />
            </div>
            <span className="text-5xl font-mono font-bold text-white tracking-wider">{mm}:{ss}</span>
            <span className="text-xs text-slate-500 mt-2">{mode.label}</span>
          </div>
        </div>
      </div>

      {/* Controls */}
      <div className="flex justify-center gap-3">
        <button onClick={reset} title="Reiniciar"
          className="p-3 rounded-xl bg-slate-800/50 border border-slate-700/50 text-slate-400 hover:text-white transition cursor-pointer">
          <RotateCcw className="w-5 h-5" />
        </button>
        <button onClick={toggleRun}
          className="px-8 py-3 rounded-xl text-white font-medium flex items-center gap-2 transition cursor-pointer"
          style={{ backgroundColor: mode.color }}>
          {running ? <><Pause className="w-5 h-5" /> Pausar</> : <><Play className="w-5 h-5" /> {seconds === total ? 'Iniciar' : 'Continuar'}</>}
        </button>
        <button onClick={skip} title="Pular"
          className="p-3 rounded-xl bg-slate-800/50 border border-slate-700/50 text-slate-400 hover:text-white transition cursor-pointer">
          <SkipForward className="w-5 h-5" />
        </button>
      </div>

      {/* Sessions info */}
      <div className="text-center space-y-3">
        <div className="flex justify-center gap-2">
          {[...Array(4)].map((_, i) => (
            <div key={i} className={`w-3 h-3 rounded-full transition ${
              i < (sessions % 4) ? 'bg-indigo-500' : 'bg-slate-700'
            }`} />
          ))}
        </div>
        <p className="text-sm text-slate-500">
          {sessions} sessão{sessions !== 1 ? 'ões' : ''} completa{sessions !== 1 ? 's' : ''}
          {sessions > 0 && sessions % 4 === 0 && ' — Hora de uma pausa longa!'}
        </p>
      </div>

      {/* Tips */}
      <div className="rounded-2xl bg-slate-800/30 border border-slate-700/30 p-5">
        <h3 className="text-sm font-semibold text-white mb-3">Como usar a Técnica Pomodoro</h3>
        <ol className="text-sm text-slate-400 space-y-1.5 list-decimal list-inside">
          <li>Escolha uma tarefa de estudo para focar.</li>
          <li>Inicie o timer de <strong className="text-white">25 minutos</strong> e estude com total concentração.</li>
          <li>Quando o timer tocar, faça uma <strong className="text-white">pausa de 5 minutos</strong>.</li>
          <li>A cada 4 sessões, faça uma <strong className="text-white">pausa longa de 15 minutos</strong>.</li>
        </ol>
      </div>
    </div>
  );
}
