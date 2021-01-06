using MifParser.Factories;
using MifParser.Interfaces;
using MifParser.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Parser.Tests
{
    
    public class ScriptFactoryTextTests
    {
        [Fact]
        public void CreateScriptTestName()
        {
            string[] lines = new string[] {"proc EllipseZ { x y z } {","set r1 [expr 2.*$x -1. ]"
                ,"set r2 [expr 2.*$y -1. ]", "set r [expr {sqrt($r1*$r1+$r2*$r2)}]","if {$r > 1.0} { return 0}"
                ,"return 1","}" };
            var factory = new ScriptFactoryText();
            IMifScript script = factory.CreateScript(lines);
            IMifScript expectedScript = new ScriptObject("EllipseZ", 
                string.Join(Environment.NewLine, lines.Skip(1).Take(lines.Length - 2)), " x y z ");
            Assert.Equal(expectedScript.Name, script.Name);
        }
        [Fact]
        public void CreateScriptTestScriptText()
        {
            string[] lines = new string[] {"proc EllipseZ { x y z } {","set r1 [expr 2.*$x -1. ]"
                ,"set r2 [expr 2.*$y -1. ]", "set r [expr {sqrt($r1*$r1+$r2*$r2)}]","if {$r > 1.0} { return 0}"
                ,"return 1","}" };
            var factory = new ScriptFactoryText();
            IMifScript script = factory.CreateScript(lines);
            IMifScript expectedScript = new ScriptObject(" EllipseZ ",
                string.Join(Environment.NewLine, lines.Skip(1).Take(lines.Length - 2)), " x y z ");
            
            Assert.Equal(expectedScript.Script, script.Script);
        }
        [Fact]
        public void CreateScriptTestArgs()
        {
            string[] lines = new string[] {"proc EllipseZ { x y z } {","set r1 [expr 2.*$x -1. ]"
                ,"set r2 [expr 2.*$y -1. ]", "set r [expr {sqrt($r1*$r1+$r2*$r2)}]","if {$r > 1.0} { return 0}"
                ,"return 1","}" };
            var factory = new ScriptFactoryText();
            IMifScript script = factory.CreateScript(lines);
            IMifScript expectedScript = new ScriptObject(" EllipseZ ",
                string.Join(Environment.NewLine, lines.Skip(1).Take(lines.Length - 2)), " x y z ");
            Assert.Equal(expectedScript.Args, script.Args);
        }
    }
}
