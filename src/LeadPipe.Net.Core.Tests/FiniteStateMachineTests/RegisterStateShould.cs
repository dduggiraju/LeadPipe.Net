// --------------------------------------------------------------------------------------------------------------------
// Copyright (c) Lead Pipe Software. All rights reserved.
// Licensed under the MIT License. Please see the LICENSE file in the project root for full license information.
// --------------------------------------------------------------------------------------------------------------------

using LeadPipe.Net.FiniteStateMachine;
using NUnit.Framework;
using System.Linq;

namespace LeadPipe.Net.Tests.FiniteStateMachineTests
{
    /// <summary>
    /// RegisterState method tests.
    /// </summary>
    [TestFixture]
    public class RegisterStateShould
    {
        /// <summary>
        /// The closed state.
        /// </summary>
        private IFiniteState closedState;

        /// <summary>
        /// The close transition.
        /// </summary>
        private IFiniteStateTransition closeTransition;

        /// <summary>
        /// The machine.
        /// </summary>
        private IFiniteStateMachine<FiniteStateMachineHistoryEntry> machine;

        /// <summary>
        /// The open state.
        /// </summary>
        private IFiniteState openState;

        /// <summary>
        /// The open transition.
        /// </summary>
        private IFiniteStateTransition openTransition;

        /// <summary>
        /// The start transition.
        /// </summary>
        private IFiniteStateTransition startTransition;

        /// <summary>
        /// Tests to make sure that a state that is not currently registered is registered.
        /// </summary>
        [Test]
        public void RegisterState()
        {
            // Act
            this.machine.RegisterState(this.openState);

            // Assert
            Assert.IsTrue(this.machine.States.Contains(this.openState));
        }

        /// <summary>
        /// Tests to make sure that an exception is thrown when trying to remove a state that is in use.
        /// </summary>
        [Test]
        public void ThrowExceptionGivenStateAlreadyRegistered()
        {
            // Act
            this.machine.RegisterState(this.openState);

            // Assert
            Assert.Throws<StateAlreadyRegisteredException>(() => this.machine.RegisterState(this.openState));
        }

        /// <summary>
        /// Runs before each test.
        /// </summary>
        [SetUp]
        public void TestSetup()
        {
            // Create our states...
            this.openState = new FiniteState(0, "Open");
            this.closedState = new FiniteState(1, "Closed");

            // Create our transitions...
            this.startTransition = new FiniteStateTransition(0, "New", new FiniteStateMachineTransitionReason("0", "New"), this.openState);

            this.openTransition = new FiniteStateTransition(1, "Open", new FiniteStateMachineTransitionReason("1", "Opened"), this.closedState, this.openState);
            this.closeTransition = new FiniteStateTransition(2, "Close", new FiniteStateMachineTransitionReason("2", "Closed"), this.openState, this.closedState);

            // Add our transitions to our states...
            this.openState.RegisterTransition(this.closeTransition);
            this.closedState.RegisterTransition(this.openTransition);

            // Create our machine...
            this.machine = new FiniteStateMachine.FiniteStateMachine(this.startTransition);
        }
    }
}