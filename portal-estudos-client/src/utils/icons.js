import {
  FlaskConical, Leaf, Atom, Apple, Microscope, Pill, HeartPulse,
  FileText, Stethoscope, Factory, Hospital, ShieldAlert, BarChart3,
  Tablets, Hexagon, TestTubes, BadgeCheck, Skull, Dna, Thermometer,
  Bug, Shield, Droplets, Activity, Bone, ScanSearch, Flower2, Globe,
  Landmark, Briefcase, Scale, BookOpen, Package, AlertTriangle,
  Utensils, Salad, Sparkles, TestTube, Beaker, Scan, CircleDot,
} from 'lucide-react';

/**
 * Mapeia o nome do ícone vindo da API para o componente Lucide correspondente.
 * Centralizado para facilitar manutenção.
 */
const iconMap = {
  'flask-conical': FlaskConical,
  'stethoscope': Stethoscope,
  'leaf': Leaf,
  'pill': Pill,
  'factory': Factory,
  'hospital': Hospital,
  'heart-pulse': HeartPulse,
  'shield-alert': ShieldAlert,
  'bar-chart-3': BarChart3,
  'tablets': Tablets,
  'atom': Atom,
  'hexagon': Hexagon,
  'test-tubes': TestTubes,
  'badge-check': BadgeCheck,
  'skull': Skull,
  'dna': Dna,
  'thermometer': Thermometer,
  'microscope': Microscope,
  'bug': Bug,
  'shield': Shield,
  'droplets': Droplets,
  'activity': Activity,
  'bone': Bone,
  'scan-search': ScanSearch,
  'flower-2': Flower2,
  'globe': Globe,
  'landmark': Landmark,
  'briefcase': Briefcase,
  'scale': Scale,
  'book-open': BookOpen,
  'package': Package,
  'apple': Apple,
  'alert-triangle': AlertTriangle,
  'utensils': Utensils,
  'salad': Salad,
  'sparkles': Sparkles,
  'test-tube': TestTube,
  'beaker': Beaker,
  'scan': Scan,
  'circle-dot': CircleDot,
};

export function getIcon(name) {
  return iconMap[name] || FileText;
}
